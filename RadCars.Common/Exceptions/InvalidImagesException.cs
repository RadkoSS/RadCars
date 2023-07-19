namespace RadCars.Common.Exceptions;

using System;

public class InvalidImagesException : Exception
{
    public InvalidImagesException(string message)
    : base(message)
    {
        
    }
}