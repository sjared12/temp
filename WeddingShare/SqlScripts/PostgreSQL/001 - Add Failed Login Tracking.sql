--
-- Table structure for table `users`
--
ALTER TABLE users ADD COLUMN failed_logins INTEGER NOT NULL DEFAULT 0;
ALTER TABLE users ADD COLUMN lockout_until TIMESTAMP;