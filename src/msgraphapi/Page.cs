using msgraphapi.ExceptionHandling;

namespace msgraphapi
{
    public class Page
    {
        public readonly int Size;
        public readonly int Number;

        public Page(int size, int number)
        {
            if (number < 1)
                throw new InvalidRequestException($"{nameof(Page)} {nameof(number)} must be greater than zero");
            if (number > 10000)
                throw new InvalidRequestException($"{nameof(Page)} {nameof(number)} must be less than 10000");
            if (size < 1)
                throw new InvalidRequestException($"{nameof(Page)} {nameof(size)} must be greater than zero");
            if (size > 10000)
                throw new InvalidRequestException($"{nameof(Page)} {nameof(size)} must be less than 10000");

            Size = size;
            Number = number;
        }
    }
}
