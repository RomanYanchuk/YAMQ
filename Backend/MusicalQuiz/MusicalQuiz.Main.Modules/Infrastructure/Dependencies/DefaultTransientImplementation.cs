using System;

namespace MusicalQuiz.Main.Modules.Infrastructure.Dependencies
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DefaultTransientImplementation : Attribute
    {
    }
}