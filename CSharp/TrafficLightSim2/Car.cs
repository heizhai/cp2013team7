using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace TrafficLightSim2
{
    [Serializable()]
    public class Car : ISerializable
    {
        private int lanePosition;
        private bool stopped = false;
        private bool lightStopped = false;
        private bool brokenDown = false;
        private bool turningLeft = false;
        private bool turningRight = false;
        public System.Windows.Forms.Timer breakdownTimer;

        public Car(int lanePosition)
        {
            this.lanePosition = lanePosition;
        }

        public Car(SerializationInfo info, StreamingContext ctxt)
        {
            this.lanePosition = (int)info.GetValue("lanePosition", typeof(int));
            this.stopped = (bool)info.GetValue("stopped", typeof(bool));
            this.lightStopped = (bool)info.GetValue("lightStopped", typeof(bool));
            this.brokenDown = (bool)info.GetValue("brokenDown", typeof(bool));
            this.turningLeft = (bool)info.GetValue("turningLeft", typeof(bool));
            this.turningRight = (bool)info.GetValue("turningRight", typeof(bool));
            this.breakdownTimer = (System.Windows.Forms.Timer)info.GetValue("breakdownTimer", typeof(System.Windows.Forms.Timer));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("lanePosition", this.lanePosition);
            info.AddValue("stopped", this.stopped);
            info.AddValue("lightStopped", this.lightStopped);
            info.AddValue("brokenDown", this.brokenDown);
            info.AddValue("turningLeft", this.turningLeft);
            info.AddValue("turningRight", this.turningRight);
            info.AddValue("breakdownTimer", this.breakdownTimer);
        }

        public bool intersects(Car car)
        {
            if (((car.getLanePosition() < (this.getLanePosition() + Settings.CAR_LENGTH)) && ((this.getLanePosition() + Settings.CAR_LENGTH) < (car.getLanePosition() + Settings.CAR_LENGTH)) || ((car.getLanePosition() < this.getLanePosition()) && (this.getLanePosition() < (car.getLanePosition() + Settings.CAR_LENGTH)))))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool intersects(int lanePosition, int laneSpeed, bool direction)
        {
            if ((((this.getLanePosition() <= lanePosition && this.getLanePosition() >= lanePosition - laneSpeed) && direction == Settings.TRAFFIC_WEST_NORTH)) || ((this.getLanePosition() + Settings.CAR_LENGTH <= lanePosition && this.getLanePosition() + Settings.CAR_LENGTH + laneSpeed >= lanePosition) && direction == Settings.TRAFFIC_EAST_SOUTH))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool intersects(Car car, int offset)
        {

            int lanePositionStart = this.getLanePosition() - Math.Abs(offset);
            int lanePositionEnd = this.getLanePosition() + Settings.CAR_LENGTH + Math.Abs(offset);

            if (((car.getLanePosition() < lanePositionEnd) && (lanePositionEnd < (car.getLanePosition() + Settings.CAR_LENGTH)) || ((lanePositionStart < this.getLanePosition()) && (lanePositionStart < (car.getLanePosition() + Settings.CAR_LENGTH)))))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean intersects(int lanePosition, Boolean direction, int offset)
        {
            if (((this.getLanePosition() - Math.Abs(offset) == lanePosition) && (direction == Settings.TRAFFIC_WEST_NORTH)) || ((lanePosition == (this.getLanePosition() + Math.Abs(offset) + Settings.CAR_LENGTH) && (direction == Settings.TRAFFIC_EAST_SOUTH))))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void moveCar(int units)
        {
            lanePosition += units;
        }

        /**
         * @return the lanePosition
         */
        public int getLanePosition()
        {
            return lanePosition;
        }

        /**
         * @param lanePosition the lanePosition to set
         */
        public void setLanePosition(int lanePosition)
        {
            this.lanePosition = lanePosition;
        }

        public void setStopped(bool stopped)
        {
            Console.WriteLine("setStopped: " + stopped);
            if (this.brokenDown != true)
            {
                this.stopped = stopped;
            }
            else
            {
                this.stopped = true;
            }
        }

        public bool getStopped()
        {
            return this.stopped;
        }

        /**
         * @return the brokenDown
         */
        public bool getBrokenDown()
        {
            return brokenDown;
        }

        /**
         * @param brokenDown the brokenDown to set
         */
        public void setBrokenDown(bool brokenDown)
        {
            this.brokenDown = brokenDown;
            if (this.brokenDown == true)
            {
                Console.WriteLine("BREAKDOWN: " + Settings.getSimSettings().getBreakdownTime());
                this.setStopped(true);
            }
            else
            {
                Console.WriteLine("BREAKDOWN END");
                this.setStopped(false);
                breakdownTimer.Stop();
            }
        }


        public void breakdown()
        {
            setBrokenDown(true);
            breakdownTimer = new System.Windows.Forms.Timer();
            breakdownTimer.Interval = Settings.getSimSettings().getBreakdownTime();
            breakdownTimer.Start();
            breakdownTimer.Tick += new EventHandler(breakdown_Tick);
        }

        public void breakdown_Tick(object sender, EventArgs eArgs)
        {
            new carBreakdownTask(this);
        }

        public void stopLight()
        {
            stopped = true;
            lightStopped = true;
        }

        public void startLight()
        {
            stopped = false;
            lightStopped = false;
        }

        /**
         * @return the lightStopped
         */
        public bool getLightStopped()
        {
            return lightStopped;
        }

        /**
         * @param lightStopped the lightStopped to set
         */
        public void setLightStopped(bool lightStopped)
        {
            this.lightStopped = lightStopped;
        }

        /**
         * @return the turningLeft
         */
        public bool getTurningLeft()
        {
            return turningLeft;
        }

        /**
         * @param turningLeft the turningLeft to set
         */
        public void setTurningLeft(bool turningLeft)
        {
            this.turningLeft = turningLeft;
        }

        /**
         * @return the turningRight
         */
        public bool getTurningRight()
        {
            return turningRight;
        }

        /**
         * @param turningRight the turningRight to set
         */
        public void setTurningRight(bool turningRight)
        {
            this.turningRight = turningRight;
        }

        public void turn(Lane turningLane, Lane originatingLane, int lanePosition)
        {
            originatingLane.removeCar(this);
            this.lanePosition = lanePosition;
            turningLane.addCar(this);
        }
    }

    class carBreakdownTask
    {

        Car car;

        public carBreakdownTask(Car car)
        {
            this.car = car;
            run();
        }

        public void run()
        {
            this.car.setBrokenDown(false);
        }
    }
}
