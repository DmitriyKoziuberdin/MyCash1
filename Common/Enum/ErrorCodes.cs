namespace Common.Enum
{
    public enum ErrorCodes
    {
        Undefined = 0,

        Account = 1_000,
        AccountNotFound = 1_001,
        //PersonDuplicateCreationSuperHeroName = 1_002,

        Transaction = 2_000,
        TransactionNotFound = 2_001,
        TransactionDuplicateCategory = 2_002,

        User = 3_000,
        UserNotFound = 3_001,
        UserDuplicate = 3_002
    }
}
