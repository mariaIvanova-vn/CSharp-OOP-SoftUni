namespace Robots.Tests
{
    using Robots;
    using NUnit.Framework;
    using System;
    using System.Linq;

    [TestFixture]
    public class RobotsTests
    {
        [Test]
        public void RobotCreatior()
        {
            Robot robot = new Robot("Richie", 100);           

            Assert.IsNotNull(robot);
            Assert.AreEqual("Richie", robot.Name);
            Assert.AreEqual(100, robot.MaximumBattery);
            Assert.AreEqual(100, robot.Battery);
        }
        [Test]
        public void RobotManager()
        {         
            RobotManager robotManager = new RobotManager(3);

            Robot robot = new Robot("Richie", 100);
            Robot robot1 = new Robot("Didi", 101);

            robotManager.Add(robot1);
            robotManager.Add(robot);

            Assert.AreEqual(2,robotManager.Count);  
            Assert.AreEqual(3, robotManager.Capacity);
        }
        [Test]
        public void RobotManagerThrowWithNegativeCapacity()
        {
            Assert.Throws<ArgumentException>(() =>{ RobotManager robotManager = new RobotManager(-1); });
        }
        [Test]
        public void RobotManagerCapacityThrowNotEnoughCapacity()
        {
            RobotManager robotManager = new RobotManager(1);

            Robot robot = new Robot("Richie", 100);
            Robot robot1 = new Robot("Didi", 101);

            robotManager.Add(robot1);
           // robotManager.Add(robot);

            Assert.Throws<InvalidOperationException>(() => robotManager.Add(robot));
        }
        [Test]
        public void RobotManagerCapacityThrow()
        {
            RobotManager robotManager = new RobotManager(5);

            Robot robot = new Robot("Richie", 100);
            Robot robot1 = new Robot("Didi", 101);

            robotManager.Add(robot1);
            robotManager.Add(robot);

            Assert.Throws<InvalidOperationException>(() => robotManager.Add(robot));
        }
        [Test]
        public void RobotManagerRemove()
        {
            RobotManager robotManager = new RobotManager(3);

            Robot robot = new Robot("Richie", 100);
            Robot robot1 = new Robot("Didi", 101);

            robotManager.Add(robot1);
            robotManager.Add(robot);

            robotManager.Remove("Didi");

            Assert.AreEqual(1, robotManager.Count);
            Assert.AreEqual(3, robotManager.Capacity);
        }
        [Test]
        public void RobotManagerRemoveThrow()
        {
            RobotManager robotManager = new RobotManager(3);

            Assert.Throws<InvalidOperationException>(() => { robotManager.Remove("gogo"); });
        }
        [Test]
        public void RobotWorkWithThrowWhenBatteryLow()
        {
            RobotManager robotManager = new RobotManager(3);

            Robot robot = new Robot("Richie", 100);
            robotManager.Add(robot);

            Assert.Throws<InvalidOperationException>(() => robotManager.Work("Becky", "Clean", 60));
        }
        [Test]
        public void RobotWorkWithThrow()
        {
            RobotManager robotManager = new RobotManager(3);

            Robot robot = new Robot("Richie", 100);
            robotManager.Add(robot);

            Assert.Throws<InvalidOperationException>(() => robotManager.Work("Richie", "Clean", 111));
        }
        [Test]
        public void RobotWork()
        {
            RobotManager robotManager = new RobotManager(3);
            Robot robot = new Robot("Richie", 100);
            robotManager.Add(robot);

            robotManager.Work("Richie", "something", 20);

            Assert.AreEqual(100, robot.MaximumBattery);
            Assert.AreEqual(80, robot.Battery);
        }
        [Test]
        public void ChargeRobot()
        {
            RobotManager robotManager = new RobotManager(3);
            Robot robot = new Robot("Richie", 100);
            robotManager.Add(robot);
            robot.Battery = 100;
            Assert.AreEqual(robot.Battery, 100);
            Assert.AreEqual(robot.Battery, robot.MaximumBattery);

            Assert.Throws<InvalidOperationException>(() => { robotManager.Charge(null); });
            Assert.Throws<InvalidOperationException>(() => { robotManager.Charge("pipi"); });
        }
        [Test]
        public void ChargeRobotToMaximumCharge()
        {
            RobotManager robotManager = new RobotManager(3);
            Robot robot = new Robot("Richie", 100);
            robotManager.Add(robot);

            robotManager.Work("Richie", "something", 20);
            Assert.AreEqual(80, robot.Battery);

            robotManager.Charge("Richie");
            Assert.AreEqual(100, robot.Battery);
        }
    }
}
