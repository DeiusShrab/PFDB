namespace DBConnect
{
  public static class PFDAL
  {
    public static IPFDBContext GetContext()
    {
      if (PFConfig.ConfigExists())
      {
        return GetContext(PFConfig.DEBUG.Equals("true", System.StringComparison.InvariantCultureIgnoreCase));
      }

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
  }
}
