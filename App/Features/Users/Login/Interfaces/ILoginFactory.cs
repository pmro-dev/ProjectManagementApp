using App.Features.Users.Login.Models;

namespace App.Features.Users.Login.Interfaces;

public interface ILoginFactory
{
	LoginInputDto CreateLoginInputDto();
}