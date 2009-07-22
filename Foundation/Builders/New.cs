namespace Foundation.Builders
{
    public static class New
    {
        public static MessageBuilder Message()
        {
            return new MessageBuilder();
        }

        public static EventBuilder Event()
        {
            return new EventBuilder();
        }
    }
}