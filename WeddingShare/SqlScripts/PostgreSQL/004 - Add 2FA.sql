--
-- Add column `2fa_token` to `users`
--
ALTER TABLE users ADD COLUMN two_fa_token TEXT;
