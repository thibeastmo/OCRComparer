using System;

namespace OCRComparer.Execution
{
    internal class TestData
    {
        public string Name { get; }
        public DateTime Creation { get; }
        public TestStatistics Statistics { get; set; }
        public bool IsGroundTruthPresent { get; set; }
        public string GroundTruthFileName 
        { 
            get
            {
                return Name + ".gt.txt";
            }
        }
        public string ResultFileName 
        { 
            get
            {
                return Name + ".txt";
            }
        }
        public string ImageFileName 
        { 
            get
            {
                return Name + ".png";
            }
        }
        public string StatisticsFileName 
        { 
            get
            {
                return Name + ".json";
            }
        }
        public bool IsResultPresent { get; set; }
        public bool IsImagePresent { get; set; }
        public bool AreStatisticsPresent { get; set; }
        public bool IsEverythingPresent 
        { get 
            { 
                return IsGroundTruthPresent && IsResultPresent && IsImagePresent && AreStatisticsPresent;
            }
        }
        public TestData(string name, DateTime creation) {
            Name = name;
            Creation = creation;
        }
    }
}
