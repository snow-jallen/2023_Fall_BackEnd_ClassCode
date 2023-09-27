using EfCoreDemo.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Recapi.Data;
using Xunit;

namespace ApiTests;

public class Class1
{
    [Fact]
    public void Pass()
    {
        Assert.True(true, "What?!");
    }

    [Fact]
    public async Task Demo()
    {
        Assert.Throws<MissingNameException>(() => new Recipe(null));        
    }
}