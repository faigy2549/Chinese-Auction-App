using auction_webapi.DAL;
using auction_webapi.Models;

namespace auction_webapi.BL
{
    public class RaffleService:IRaffleService

    {
        private IRaffleDAL _raffleDAL;
        private readonly IEmailService _emailService;
        public RaffleService(IRaffleDAL raffleDAL, IEmailService emailService)
        {
            this._raffleDAL = raffleDAL ?? throw new ArgumentNullException(nameof(raffleDAL));
            this._emailService = emailService;
        }

        public async Task<Winner> Raffle(Present p)
        {
            Winner winner = await _raffleDAL.Raffle(p);
            string subject = "Congratulations " + winner.User.FirstName + "! You've Won at F🪙R!";
            string body = $@"
            Hi {winner.User.FirstName},

            We are thrilled to inform you that you are the winner of our latest raffle at F🪙R! This is an incredible achievement, and we couldn't be more excited to share this news with you.

            As our lucky winner of a {winner.Present.Name}, Here’s what you can expect next:

            1. **Contact from Our Team:** Our team will reach out to you within the next 24 hours to provide further details and guide you through the process.
            2. **Prize Delivery:** We will arrange the delivery of your prize to ensure it reaches you as quickly as possible.
            3. **Celebrate:** Get ready to celebrate and enjoy your prize!

            Thank you for participating in our raffle and being a valued member of the F🪙R community. We love rewarding our loyal participants, and your victory is a testament to the spirit of fun and excitement we aim to bring to our community.

            If you have any questions or need further assistance, please do not hesitate to contact us at support@for-eachother.inc. We are here to help!

            Congratulations once again, and we look forward to celebrating your win with you!

            Best regards,

            The F🪙R Team
            FoR eachother.INC
            ";
            await _emailService.SendEmailAsync(winner.Email, subject, body);
            return winner;
        }
        public async Task<string> Results()
        {
            return await _raffleDAL.Results();
        }
        public async Task<string> Income()
        {
            return await _raffleDAL.Income();
        }
    }
}
