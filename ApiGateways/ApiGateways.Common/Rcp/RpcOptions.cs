namespace Common.Rcp
{
    public class RpcOptions
    {
        public RpcOptions(string queueName)
        {
            QueueName = queueName;
            Host = SettingsConfigHelper.GetCurrentSettings("RpcServer", "Host").AppSettingValue;
            UserName = SettingsConfigHelper.GetCurrentSettings("RpcServer", "UserName").AppSettingValue;
            Password = SettingsConfigHelper.GetCurrentSettings("RpcServer", "Password").AppSettingValue;
        }

        public string Host { get; set; }
        public string QueueName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
