namespace DBConnect
{
  public static class PFDAL
  {
    public static IPFDBContext GetContext()
    {
#if DEBUG
      return GetContext(true);
#else
      return GetContext(false);
#endif
    }

    public static IPFDBContext GetContext(bool isTest)
    {
      if (isTest)
        return new TestContext();

      return new PFDBContext();
    }

    public static IPFDBContext GetContext(string environment)
    {
      return GetContext(!environment.Equals(Env.LIVE.ToString()));
    }

    public enum Env
    {
      LIVE,
      TEST
    }
  }
}
