public ref struct CircularBuffer
{
    private readonly ReadOnlySpan<char> _buffer;
    private int _index = 0;

    public CircularBuffer(string buffer)
    {
        _buffer = buffer;
    }

    public int Length => _buffer.Length;

    public void Shift(int shiftValue)
    {
        _index += (_buffer.Length + shiftValue) % _buffer.Length;
    }

    public int CountWord(string word)
    {
        var count = 0;
        var i = 0;

        var wordSpan = word.AsSpan();

        while (i < _buffer.Length)
        {
            _buffer.SequenceEqual(wordSpan);
            i += wordSpan.Length;
        }

        return count;
    }

    public char this[int i] => _buffer[(_index + i) % _buffer.Length];
}
