--
-- Table structure for table `galleries`
--
DROP TABLE IF EXISTS "galleries";
CREATE TABLE "galleries" (
  "id" SERIAL PRIMARY KEY,
  "name" TEXT NOT NULL UNIQUE,
  "secret_key" TEXT NULL
);

DROP TABLE IF EXISTS "gallery_items";
CREATE TABLE "gallery_items" (
  "id" SERIAL PRIMARY KEY,
  "gallery_id" INTEGER NOT NULL,
  "title" TEXT NOT NULL,
  "uploaded_by" TEXT NULL,
  "state" INTEGER NOT NULL DEFAULT 0,
  FOREIGN KEY ("gallery_id") REFERENCES "galleries" ("id") 
);

INSERT INTO "galleries" 
    ("id", "name", "secret_key")
VALUES 
    (1, 'default', NULL);

--
-- Table structure for table `users`
--
DROP TABLE IF EXISTS "users";
CREATE TABLE "users" (
  "id" SERIAL PRIMARY KEY,
  "username" TEXT NOT NULL UNIQUE,
  "email" TEXT NULL UNIQUE,
  "password" TEXT NOT NULL
);

ALTER TABLE "users" ADD COLUMN "role" TEXT NOT NULL DEFAULT 'user';

INSERT INTO "users" 
VALUES 
  (1, 'admin', NULL, 'admin', 'admin');