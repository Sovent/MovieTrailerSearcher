using System;
using System.Runtime.Serialization;

namespace TmdbGateway
{
    [Serializable]
    public class TmdbException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public TmdbException()
        {
        }

        public TmdbException(string message) : base(message)
        {
        }

        public TmdbException(string message, Exception inner) : base(message, inner)
        {
        }

        protected TmdbException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}