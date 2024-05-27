namespace Common.Enum
{
    public enum ErrorCodes
    {
        Undefined = 0,

        Person = 1_000,
        PersonNotFound = 1_001,
        PersonDuplicateCreationSuperHeroName = 1_002,

        Category = 2_000,
        CategoryNotFound = 2_001,
        CategoryDuplicateName = 2_002
    }
}
