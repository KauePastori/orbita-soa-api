namespace Orbita.SoaApi.Domain.Exceptions
{
    public class UnauthorizedException : DomainException
    {
        public UnauthorizedException(string message) : base(message) { }
    }
}
