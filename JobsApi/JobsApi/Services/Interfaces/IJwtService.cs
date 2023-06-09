﻿using JobsApi.Dtos;

namespace JobsApi.Services.Interfaces;

public interface IJwtService
{
    string GenerateUserToken(UserDto user);
}