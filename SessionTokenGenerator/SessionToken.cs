using System;

namespace Global.SessionTokenGeneratorPack
{
    public class SessionToken
    {
        public DateTime dateTime { get; set; }
        public int daysToExpire { get; set; } = 1;
        public string UUID { get; set; }
        public int userId { get; set; }
        public string userName { get; set; }

        public bool IsValid(string deviceId)
        {
            if(daysToExpire == 0){
                return this.UUID == deviceId;
            }

            DateTime expirationDate = dateTime.AddDays(daysToExpire);
            return (expirationDate > DateTime.Now && this.UUID == deviceId);
        }
    }
}
