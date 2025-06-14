using BankApi.DTOS;
using System;

public static class UserMapper
{
    public static UserDTOS toUserDTO(this UserDTOS userModel)
    {
        return new UserDTOS
        {
            ID = userModel.ID,
            email = userModel.email,
            login = userModel.login,
            password = userModel.password,

        };
    }
}
