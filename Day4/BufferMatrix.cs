namespace Day4;

internal class BufferMatrix
{
    private readonly IEnumerable<CircularBuffer> _buffers;

    public BufferMatrix(IEnumerable<CircularBuffer> buffers)
    {
        _buffers = buffers;
    }

    public int CountWord(string word)
    {
        var count = 0;

        //for (var i = 0; i < _buffers.First().Length; i++)
        //{
        //    count += _buffers.Select(buffer =>
        //    {
        //        buffer.Shift(i);
        //        return buffer.CountWord(word);
        //    }).Sum();
        //}

        return count;
    }
}
