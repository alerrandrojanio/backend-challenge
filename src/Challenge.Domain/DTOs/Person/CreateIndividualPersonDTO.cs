﻿namespace Challenge.Domain.DTOs.Person;

public class CreateIndividualPersonDTO
{
    public string Name { get; set; } = string.Empty;

    public string CPF { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public DateTime BirthDate { get; set; }
}
