﻿namespace EChallanSystem.Constants
{
    public class Authorization
    {
        public enum Roles
        {
            Manager,
            Citizen,
            TrafficWarden
        }
        public const string default_username = "user";
        public const string default_email = "user@secureapi.com";
        public const string default_password = "Pa$$w0rd.";
        public const Roles default_role = Roles.Manager;
    }
}
