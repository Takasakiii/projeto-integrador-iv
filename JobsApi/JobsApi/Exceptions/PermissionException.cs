﻿namespace JobsApi.Exceptions;

public class PermissionException : Exception
{
    public PermissionException(string message) : base(message)
    {
    }
}