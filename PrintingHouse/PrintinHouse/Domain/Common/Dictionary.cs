namespace PrintingHouse.Domain.Common
{
    public class EmailTemplates
    {
        public const int EmailVerification = 1;
    }
    public class OrderStatuses
    {
        public const int Pending = 1;
        public const int Failed = 2;
        public const int Packing = 3;
        public const int Delivering = 4;
        public const int Finished = 5;
    }
}
