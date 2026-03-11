namespace TaskifyApi.Application.Exceptions;

/// <summary>
/// Исключение, содержащее одну или несколько ошибок валидации.
/// </summary>
public abstract class ValidationException(IReadOnlyDictionary<string, string[]> errors)
    : Exception("Произошла одна или несколько ошибок валидации.")
{
    /// <summary>
    /// Словарь, где ключ - это имя свойства, а значение - массив сообщений об ошибках для этого свойства.
    /// </summary>
    public IReadOnlyDictionary<string, string[]> Errors { get; } = errors;

    // Удобный конструктор для одной ошибки
    public ValidationException(string field, string message)
        : this(new Dictionary<string, string[]> { { field, [message] } })
    {
    }
}