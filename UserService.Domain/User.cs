namespace UserService.Domain;

/// <summary>
/// Пользователь.
/// </summary>
public class User
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Имя.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Отчество.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Фамилия.
    /// </summary>
    public string? MiddleName { get; set; }

    /// <summary>
    /// Дата рождения.
    /// </summary>
    public DateOnly? BirthDay { get; set; }

    /// <summary>
    /// Серия и номер паспорта.
    /// </summary>
    public string? PassportId { get; set; }

    /// <summary>
    /// Место рождения.
    /// </summary>
    public string? BirthPlace { get; set; }

    /// <summary>
    /// Номер телефона.
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// Почта.
    /// </summary>
    public string? Email { get; set; }

    //NOTE: В качестве тестового не стал выносить адреса в таблицы(один ко многим)
    //+ по хорошему провалидировать на корректность если эти данные вводятся,

    /// <summary>
    /// Адресс регистрации.
    /// </summary>
    public string? RegistrationAddress { get; set; }

    /// <summary>
    /// Адрес проживания.
    /// </summary>
    public string? LivingAddress { get; set; }
}
