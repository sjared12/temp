--
-- Add column `media_type` to `gallery_items`
--
ALTER TABLE gallery_items ADD COLUMN media_type INTEGER NOT NULL DEFAULT 1;