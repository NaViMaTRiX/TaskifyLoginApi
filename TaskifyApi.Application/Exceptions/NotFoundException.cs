namespace TaskifyApi.Application.Exceptions;

// Не забудьте определить ваши кастомные исключения
/// <summary>
/// Исключение, выбрасываемое, когда запрашиваемый ресурс не найден.
/// </summary>
public abstract class NotFoundException : Exception
{
    /// <summary>
    /// Название ресурса, который не был найден (например, "Пользователь", "Заказ").
    /// </summary>
    public string ResourceName { get; }

    /// <summary>
    /// Ключ или идентификатор, по которому искали ресурс (например, ID, email).
    /// </summary>
    public object ResourceKey { get; }

    // ✨ Конструктор для создания исключения с полной информацией.
    protected NotFoundException(string resourceName, object resourceKey)
        // Формируем информативное сообщение для логов
        : base($"Ресурс '{resourceName}' с ключом '{resourceKey}' не найден.")
    {
        ResourceName = resourceName;
        ResourceKey = resourceKey;
    }

    // Конструктор для совместимости
    protected NotFoundException(string message) : base(message)
    {
        ResourceName = "Неизвестный ресурс";
        ResourceKey = "неизвестный ключ";
    }
}