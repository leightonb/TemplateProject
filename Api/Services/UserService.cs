using TemplateProject.Repositories.Models;
using System.Security.Cryptography;
using TemplateProject.Models;
using TemplateProject.Repositories;

namespace TemplateProject.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<User?> Register(RegisterUser userModel)
    {
        // validate user
        if (string.IsNullOrWhiteSpace(userModel.Email)) throw new BadHttpRequestException("An email address is required");
        if (string.IsNullOrWhiteSpace(userModel.Password)) throw new BadHttpRequestException("A password is required");
        if (!string.Equals(userModel.Password, userModel.ConfirmPassword)) throw new BadHttpRequestException("Passwords do not match");
        if (string.IsNullOrWhiteSpace(userModel.FirstName)) throw new BadHttpRequestException("A first name is required");
        if (string.IsNullOrWhiteSpace(userModel.LastName)) throw new BadHttpRequestException("A last name is required");

        // validate unique
        var emailMatch = await GetByEmail(userModel.Email);
        if (emailMatch != null) throw new BadHttpRequestException("A user with that email address already exists");
        if (!string.IsNullOrWhiteSpace(userModel.Username))
        {
            var usernameMatch = await GetByUsername(userModel.Username);
            if (usernameMatch != null) throw new BadHttpRequestException("That username is unavailable");
        }
        else userModel.Username = userModel.Email;

        using Aes aes = Aes.Create();

        byte[] encrypted = EncryptStringToBytes_Aes(userModel.Password, aes.Key, aes.IV);
        var user = new User
        {
            Email = userModel.Email,
            FirstName = userModel.FirstName,
            LastName = userModel.LastName,
            Mobile = userModel.Mobile,
            Password = encrypted,
            PasswordKey = aes.Key,
            PasswordIV = aes.IV,
            Phone = userModel.Phone,
            Status = Enums.EntityStatus.New,
            Username = userModel.Username
        };

        user = await userRepository.Create(user);

        return user;
    }

    public async Task<User?> Authenticate(string username, string password)
    {
        var user = await userRepository.GetByUsername(username);

        if (user == null) return null;

        using Aes aes = Aes.Create();

        var encrypted = EncryptStringToBytes_Aes(password, user.PasswordKey, user.PasswordIV);

        if (user.Password.SequenceEqual(encrypted))
        {
            return user;
        }

        return null;
    }

    public async Task<User?> GetById(int id)
    {
        return await userRepository.GetById(id);
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await userRepository.GetByEmail(email);
    }

    public async Task<User?> GetByUsername(string username)
    {
        return await userRepository.GetByUsername(username);
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        return await userRepository.GetAll();
    }

    static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
    {
        if (plainText == null || plainText.Length <= 0)
            throw new ArgumentNullException(nameof(plainText));
        if (Key == null || Key.Length <= 0)
            throw new ArgumentNullException(nameof(Key));
        if (IV == null || IV.Length <= 0)
            throw new ArgumentNullException(nameof(IV));
        byte[] encrypted;

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using MemoryStream msEncrypt = new();
            using CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write);
            using (StreamWriter swEncrypt = new(csEncrypt))
            {
                swEncrypt.Write(plainText);
            }
            encrypted = msEncrypt.ToArray();
        }

        return encrypted;
    }
}