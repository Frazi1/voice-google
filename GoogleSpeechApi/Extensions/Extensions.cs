using System;

namespace GoogleSpeechApi.Extensions
{
    public static class Extensions
    {
        public static TResult Transform<TInput, TResult>(this TInput o, Func<TInput, TResult> transormer)
        {
            return transormer(o);
        }
    }
}