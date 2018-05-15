using Ninject;

namespace GoogleSpeechApi.DI
{
    public static class Kernel
    {
        private static IKernel _instance;
        public static IKernel Instance {
            get {
                if (_instance == null)
                {
                    _instance = new StandardKernel();
                    _instance.Load(new DiModule());
                }
                return _instance;
            }
        }
    }
}