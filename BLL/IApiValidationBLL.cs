using DTO;
using System;

namespace BLL
{
    public interface IApiValidationBLL
    {

        public void ValidateUserCredentials(User user, string password);
        public void ValidateAndUpdateNewUserCredentials(User user);
        public string HashPassword(string inputString);
        public string GenerateToken(User user);
        public int ExtractUserIdFromToken(string token);
        public void ValidateId(string token, int id);
        //public void ValidateNewCartProduct(ProductDetails product, CartProduct cartProduct);
        //public bool ValidateAndUpdateNewPurchase(ProductDetails[] requestedProducts, Purchase purchase);
        //public void ValidatePurchaseRequest(string token, Purchase requestedPurchase);
        //public void ValidateAndUpdateNewProduct(ProductDetails product, bool updateExisting = false);
        ////public Cart UpdateCart(ProductDetails[] productsInCart, Cart cart);
    }
}
