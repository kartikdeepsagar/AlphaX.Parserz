namespace AlphaX.Parserz
{
    /// <summary>
    /// Interface for cloning items of type <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICloneable<T>
    {
        /// <summary>
        /// Creates a clone of <typeparamref name="T"/>.
        /// </summary>
        /// <returns></returns>
        T Clone();
    }
}
