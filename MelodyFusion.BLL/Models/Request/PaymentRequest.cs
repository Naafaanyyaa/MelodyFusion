using System.ComponentModel.DataAnnotations;

namespace MelodyFusion.BLL.Models.Request
{

    public class PaymentRequest
    {
        [Required]
        public string UserId { get; set; } = string.Empty;
        /// <summary>
        /// Payment method nonce obtained from the client-side.
        /// </summary>
        [Required]
        public string PaymentMethodNonce { get; set; } = string.Empty;
        [Required]
        public decimal Amount { get; set; }
    }
}
