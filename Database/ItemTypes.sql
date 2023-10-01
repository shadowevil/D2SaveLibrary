/*
 Navicat Premium Data Transfer

 Source Server         : D2SLibData
 Source Server Type    : SQLite
 Source Server Version : 3035005 (3.35.5)
 Source Schema         : main

 Target Server Type    : SQLite
 Target Server Version : 3035005 (3.35.5)
 File Encoding         : 65001

 Date: 01/10/2023 01:04:31
*/

PRAGMA foreign_keys = false;

-- ----------------------------
-- Table structure for ItemTypes
-- ----------------------------
DROP TABLE IF EXISTS "ItemTypes";
CREATE TABLE "ItemTypes" (
  "ItemType" TEXT,
  "Code" TEXT,
  "Equiv1" TEXT,
  "Equiv2" TEXT,
  "Repair" REAL,
  "Body" REAL,
  "BodyLoc1" TEXT,
  "BodyLoc2" TEXT,
  "Shoots" TEXT,
  "Quiver" TEXT,
  "Throwable" REAL,
  "Reload" REAL,
  "ReEquip" REAL,
  "AutoStack" REAL,
  "Magic" REAL,
  "Rare" REAL,
  "Normal" REAL,
  "Beltable" REAL,
  "MaxSockets1" REAL,
  "MaxSocketsLevelThreshold1" REAL,
  "MaxSockets2" REAL,
  "MaxSocketsLevelThreshold2" REAL,
  "MaxSockets3" REAL,
  "TreasureClass" REAL,
  "Rarity" REAL,
  "StaffMods" TEXT,
  "Class" TEXT,
  "VarInvGfx" REAL,
  "InvGfx1" TEXT,
  "InvGfx2" TEXT,
  "InvGfx3" TEXT,
  "InvGfx4" TEXT,
  "InvGfx5" TEXT,
  "InvGfx6" TEXT,
  "StorePage" TEXT,
  "eol" REAL,
  PRIMARY KEY ("Code")
);

-- ----------------------------
-- Records of ItemTypes
-- ----------------------------
INSERT INTO "ItemTypes" VALUES ('None', 'none', NULL, NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, NULL, 0.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0.0);
INSERT INTO "ItemTypes" VALUES ('Quest', 'ques', NULL, NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, NULL, 0.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0.0);
INSERT INTO "ItemTypes" VALUES ('Weapon', 'weap', NULL, NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 1.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0.0);
INSERT INTO "ItemTypes" VALUES ('Melee Weapon', 'mele', 'weap', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 1.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0.0);
INSERT INTO "ItemTypes" VALUES ('Missile Weapon', 'miss', 'weap', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0.0);
INSERT INTO "ItemTypes" VALUES ('Thrown Weapon', 'thro', 'weap', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 1.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0.0);
INSERT INTO "ItemTypes" VALUES ('Combo Weapon', 'comb', 'mele', 'thro', 0.0, 0.0, NULL, NULL, NULL, NULL, 1.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0.0);
INSERT INTO "ItemTypes" VALUES ('Any Armor', 'armo', NULL, NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 1.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0.0);
INSERT INTO "ItemTypes" VALUES ('Miscellaneous', 'misc', NULL, NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0.0);
INSERT INTO "ItemTypes" VALUES ('Socket Filler', 'sock', 'misc', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, NULL, 0.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0.0);
INSERT INTO "ItemTypes" VALUES ('Second Hand', 'seco', NULL, NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0.0);
INSERT INTO "ItemTypes" VALUES ('Missile', 'misl', 'misc', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, NULL, 0.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0.0);
INSERT INTO "ItemTypes" VALUES ('Blunt', 'blun', 'mele', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0.0);
INSERT INTO "ItemTypes" VALUES ('Jewel', 'jewl', 'sock', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, 1.0, 1.0, 0.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 6.0, 'invjw1', 'invjw2', 'invjw3', 'invjw4', 'invjw5', 'invjw6', 'misc', 0.0);
INSERT INTO "ItemTypes" VALUES ('Class Specific', 'clas', NULL, NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0.0);
INSERT INTO "ItemTypes" VALUES ('Amazon Item', 'amaz', 'clas', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 1.0, NULL, 'ama', 0.0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0.0);
INSERT INTO "ItemTypes" VALUES ('Barbarian Item', 'barb', 'clas', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 1.0, NULL, 'bar', 0.0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0.0);
INSERT INTO "ItemTypes" VALUES ('Necromancer Item', 'necr', 'clas', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 1.0, NULL, 'nec', 0.0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0.0);
INSERT INTO "ItemTypes" VALUES ('Paladin Item', 'pala', 'clas', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 1.0, NULL, 'pal', 0.0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0.0);
INSERT INTO "ItemTypes" VALUES ('Sorceress Item', 'sorc', 'clas', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 1.0, NULL, 'sor', 0.0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0.0);
INSERT INTO "ItemTypes" VALUES ('Assassin Item', 'assn', 'clas', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 2.0, NULL, 'ass', 0.0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0.0);
INSERT INTO "ItemTypes" VALUES ('Druid Item', 'drui', 'clas', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 1.0, NULL, 'dru', 0.0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0.0);
INSERT INTO "ItemTypes" VALUES ('Hand to Hand', 'h2h', 'mele', 'assn', 1.0, 1.0, 'rarm', 'larm', NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 2.0, 25.0, 3.0, 40.0, 3.0, 0.0, 2.0, NULL, 'ass', 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'weap', 0.0);
INSERT INTO "ItemTypes" VALUES ('Orb', 'orb', 'weap', 'sorc', 1.0, 1.0, 'rarm', 'larm', NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 2.0, 25.0, 3.0, 40.0, 3.0, 0.0, 1.0, 'sor', 'sor', 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'weap', 0.0);
INSERT INTO "ItemTypes" VALUES ('Rune', 'rune', 'sock', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, NULL, 1.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'misc', 0.0);
INSERT INTO "ItemTypes" VALUES ('Hand to Hand 2', 'h2h2', 'h2h', NULL, 1.0, 1.0, 'rarm', 'larm', NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 2.0, 25.0, 3.0, 40.0, 3.0, 0.0, 2.0, 'ass', 'ass', 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'weap', 0.0);
INSERT INTO "ItemTypes" VALUES ('Swords and Knives', 'blde', 'mele', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0.0);
INSERT INTO "ItemTypes" VALUES ('Spears and Polearms', 'sppl', 'mele', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0.0);
INSERT INTO "ItemTypes" VALUES ('Armor', 'tors', 'armo', NULL, 1.0, 1.0, 'tors', 'tors', NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 3.0, 25.0, 4.0, 40.0, 6.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'armo', 0.0);
INSERT INTO "ItemTypes" VALUES ('Gold', 'gold', 'misc', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, NULL, 1.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0.0);
INSERT INTO "ItemTypes" VALUES ('Bow Quiver', 'bowq', 'misl', 'seco', 0.0, 1.0, 'rarm', 'larm', NULL, 'bow', 0.0, 1.0, 0.0, 1.0, NULL, NULL, 1.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'misc', 0.0);
INSERT INTO "ItemTypes" VALUES ('Crossbow Quiver', 'xboq', 'misl', 'seco', 0.0, 1.0, 'rarm', 'larm', NULL, 'xbow', 0.0, 1.0, 0.0, 1.0, NULL, NULL, 1.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'misc', 0.0);
INSERT INTO "ItemTypes" VALUES ('Player Body Part', 'play', 'misc', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, NULL, 1.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'misc', 0.0);
INSERT INTO "ItemTypes" VALUES ('Herb', 'herb', 'misc', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, NULL, 0.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'misc', 0.0);
INSERT INTO "ItemTypes" VALUES ('Potion', 'poti', 'misc', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, NULL, 1.0, 1.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'misc', 0.0);
INSERT INTO "ItemTypes" VALUES ('Ring', 'ring', 'misc', NULL, 0.0, 1.0, 'rrin', 'lrin', NULL, NULL, 0.0, 0.0, 0.0, 0.0, 1.0, 1.0, 0.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 5.0, 'invrin1', 'invrin2', 'invrin3', 'invrin4', 'invrin5', NULL, 'misc', 0.0);
INSERT INTO "ItemTypes" VALUES ('Elixir', 'elix', 'misc', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, NULL, 0.0, 1.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'misc', 0.0);
INSERT INTO "ItemTypes" VALUES ('Amulet', 'amul', 'misc', NULL, 0.0, 1.0, 'neck', 'neck', NULL, NULL, 0.0, 0.0, 0.0, 0.0, 1.0, 1.0, 0.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 3.0, 'invamu1', 'invamu2', 'invamu3', NULL, NULL, NULL, 'misc', 0.0);
INSERT INTO "ItemTypes" VALUES ('Charm', 'char', 'misc', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, 1.0, NULL, 0.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 3.0, 'invch1', 'invch4', 'invch7', NULL, NULL, NULL, 'misc', 0.0);
INSERT INTO "ItemTypes" VALUES ('Boots', 'boot', 'armo', NULL, 1.0, 1.0, 'feet', 'feet', NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'armo', 0.0);
INSERT INTO "ItemTypes" VALUES ('Gloves', 'glov', 'armo', NULL, 1.0, 1.0, 'glov', 'glov', NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'armo', 0.0);
INSERT INTO "ItemTypes" VALUES ('Book', 'book', 'misc', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, NULL, 1.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'misc', 0.0);
INSERT INTO "ItemTypes" VALUES ('Belt', 'belt', 'armo', NULL, 1.0, 1.0, 'belt', 'belt', NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'armo', 0.0);
INSERT INTO "ItemTypes" VALUES ('Gem', 'gem', 'sock', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, NULL, 1.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'misc', 0.0);
INSERT INTO "ItemTypes" VALUES ('Torch', 'torc', 'misc', NULL, 0.0, 1.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, NULL, 1.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'misc', 0.0);
INSERT INTO "ItemTypes" VALUES ('Scroll', 'scro', 'misc', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, NULL, 1.0, 1.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'misc', 0.0);
INSERT INTO "ItemTypes" VALUES ('Bow', 'bow', 'miss', NULL, 0.0, 1.0, 'rarm', 'larm', 'bowq', NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 3.0, 25.0, 4.0, 40.0, 6.0, 1.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'weap', 0.0);
INSERT INTO "ItemTypes" VALUES ('Axe', 'axe', 'mele', NULL, 1.0, 1.0, 'rarm', 'larm', NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 4.0, 25.0, 5.0, 40.0, 6.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'weap', 0.0);
INSERT INTO "ItemTypes" VALUES ('Club', 'club', 'blun', NULL, 1.0, 1.0, 'rarm', 'larm', NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 3.0, 25.0, 4.0, 40.0, 6.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'weap', 0.0);
INSERT INTO "ItemTypes" VALUES ('Sword', 'swor', 'blde', NULL, 1.0, 1.0, 'rarm', 'larm', NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 3.0, 25.0, 4.0, 40.0, 6.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'weap', 0.0);
INSERT INTO "ItemTypes" VALUES ('Hammer', 'hamm', 'blun', NULL, 1.0, 1.0, 'rarm', 'larm', NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 3.0, 25.0, 4.0, 40.0, 6.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'weap', 0.0);
INSERT INTO "ItemTypes" VALUES ('Knife', 'knif', 'blde', NULL, 1.0, 1.0, 'rarm', 'larm', NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 2.0, 25.0, 3.0, 40.0, 3.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'weap', 0.0);
INSERT INTO "ItemTypes" VALUES ('Spear', 'spea', 'sppl', NULL, 1.0, 1.0, 'rarm', 'larm', NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 3.0, 25.0, 4.0, 40.0, 6.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'weap', 0.0);
INSERT INTO "ItemTypes" VALUES ('Polearm', 'pole', 'sppl', NULL, 1.0, 1.0, 'rarm', 'larm', NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 3.0, 25.0, 4.0, 40.0, 6.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'weap', 0.0);
INSERT INTO "ItemTypes" VALUES ('Crossbow', 'xbow', 'miss', NULL, 1.0, 1.0, 'rarm', 'larm', 'xboq', NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 3.0, 25.0, 4.0, 40.0, 6.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'weap', 0.0);
INSERT INTO "ItemTypes" VALUES ('Mace', 'mace', 'blun', NULL, 1.0, 1.0, 'rarm', 'larm', NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 3.0, 25.0, 4.0, 40.0, 6.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'weap', 0.0);
INSERT INTO "ItemTypes" VALUES ('Helm', 'helm', 'armo', NULL, 1.0, 1.0, 'head', 'head', NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 2.0, 25.0, 2.0, 40.0, 3.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'armo', 0.0);
INSERT INTO "ItemTypes" VALUES ('Missile Potion', 'tpot', 'thro', NULL, 0.0, 1.0, 'rarm', 'larm', NULL, NULL, 1.0, 1.0, 1.0, 1.0, NULL, NULL, 1.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'misc', 0.0);
INSERT INTO "ItemTypes" VALUES ('Body Part', 'body', 'misc', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, NULL, 1.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'misc', 0.0);
INSERT INTO "ItemTypes" VALUES ('Key', 'key', 'misc', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 1.0, NULL, NULL, 1.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'misc', 0.0);
INSERT INTO "ItemTypes" VALUES ('Throwing Knife', 'tkni', 'comb', 'knif', 1.0, 1.0, 'rarm', 'larm', NULL, NULL, 1.0, 1.0, 1.0, 1.0, NULL, 1.0, 0.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'misc', 0.0);
INSERT INTO "ItemTypes" VALUES ('Throwing Axe', 'taxe', 'comb', 'axe', 1.0, 1.0, 'rarm', 'larm', NULL, NULL, 1.0, 1.0, 1.0, 1.0, NULL, 1.0, 0.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'misc', 0.0);
INSERT INTO "ItemTypes" VALUES ('Javelin', 'jave', 'comb', 'spea', 1.0, 1.0, 'rarm', 'larm', NULL, NULL, 1.0, 1.0, 1.0, 1.0, NULL, 1.0, 0.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'misc', 0.0);
INSERT INTO "ItemTypes" VALUES ('Any Shield', 'shld', 'armo', 'seco', 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0.0);
INSERT INTO "ItemTypes" VALUES ('Staves And Rods', 'rod', 'blun', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0.0);
INSERT INTO "ItemTypes" VALUES ('Voodoo Heads', 'head', 'shld', 'necr', 1.0, 1.0, 'rarm', 'larm', NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 2.0, 25.0, 3.0, 40.0, 3.0, 0.0, 1.0, 'nec', 'nec', 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'armo', 0.0);
INSERT INTO "ItemTypes" VALUES ('Auric Shields', 'ashd', 'shld', 'pala', 1.0, 1.0, 'rarm', 'larm', NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 3.0, 25.0, 4.0, 40.0, 4.0, 0.0, 1.0, NULL, 'pal', 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'armo', 0.0);
INSERT INTO "ItemTypes" VALUES ('Primal Helm', 'phlm', 'helm', 'barb', 1.0, 1.0, 'head', 'head', NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 2.0, 25.0, 3.0, 40.0, 3.0, 0.0, 1.0, 'bar', 'bar', 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'armo', 0.0);
INSERT INTO "ItemTypes" VALUES ('Pelt', 'pelt', 'helm', 'drui', 1.0, 1.0, 'head', 'head', NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 2.0, 25.0, 3.0, 40.0, 3.0, 0.0, 1.0, 'dru', 'dru', 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'armo', 0.0);
INSERT INTO "ItemTypes" VALUES ('Cloak', 'cloa', 'tors', 'assn', 1.0, 1.0, 'tors', 'tors', NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 1.0, 'ass', 'ass', 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'armo', 0.0);
INSERT INTO "ItemTypes" VALUES ('Circlet', 'circ', 'helm', NULL, 1.0, 1.0, 'head', 'head', NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 1.0, 25.0, 2.0, 40.0, 3.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'armo', 0.0);
INSERT INTO "ItemTypes" VALUES ('Healing Potion', 'hpot', 'poti', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, NULL, 1.0, 1.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'misc', 0.0);
INSERT INTO "ItemTypes" VALUES ('Mana Potion', 'mpot', 'poti', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, NULL, 1.0, 1.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'misc', 0.0);
INSERT INTO "ItemTypes" VALUES ('Rejuv Potion', 'rpot', 'hpot', 'mpot', 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, NULL, 1.0, 1.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'misc', 0.0);
INSERT INTO "ItemTypes" VALUES ('Stamina Potion', 'spot', 'poti', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, NULL, 1.0, 1.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'misc', 0.0);
INSERT INTO "ItemTypes" VALUES ('Antidote Potion', 'apot', 'poti', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, NULL, 1.0, 1.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'misc', 0.0);
INSERT INTO "ItemTypes" VALUES ('Thawing Potion', 'wpot', 'poti', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, NULL, 1.0, 1.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'misc', 0.0);
INSERT INTO "ItemTypes" VALUES ('Small Charm', 'scha', 'char', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, 1.0, NULL, 0.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 3.0, 'invch1', 'invch4', 'invch7', NULL, NULL, NULL, 'misc', 0.0);
INSERT INTO "ItemTypes" VALUES ('Medium Charm', 'mcha', 'char', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, 1.0, NULL, 0.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 3.0, 'invch2', 'invch5', 'invch8', NULL, NULL, NULL, 'misc', 0.0);
INSERT INTO "ItemTypes" VALUES ('Large Charm', 'lcha', 'char', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, 1.0, NULL, 0.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 3.0, 'invch3', 'invch6', 'invch9', NULL, NULL, NULL, 'misc', 0.0);
INSERT INTO "ItemTypes" VALUES ('Amazon Bow', 'abow', 'bow', 'amaz', 0.0, 1.0, 'rarm', 'larm', 'bowq', NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 3.0, 25.0, 4.0, 40.0, 5.0, 1.0, 1.0, NULL, 'ama', 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'weap', 0.0);
INSERT INTO "ItemTypes" VALUES ('Amazon Spear', 'aspe', 'spea', 'amaz', 1.0, 1.0, 'rarm', 'larm', NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 3.0, 25.0, 4.0, 40.0, 6.0, 0.0, 1.0, NULL, 'ama', 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'weap', 0.0);
INSERT INTO "ItemTypes" VALUES ('Amazon Javelin', 'ajav', 'jave', 'amaz', 1.0, 1.0, 'rarm', 'larm', NULL, NULL, 1.0, 1.0, 1.0, 1.0, NULL, 1.0, 0.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 1.0, NULL, 'ama', 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'misc', 0.0);
INSERT INTO "ItemTypes" VALUES ('Magic Bow Quiv', 'mboq', 'bowq', NULL, 0.0, 1.0, 'rarm', 'larm', NULL, 'bow', 0.0, 1.0, 0.0, 0.0, 1.0, 1.0, 0.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'misc', 0.0);
INSERT INTO "ItemTypes" VALUES ('Magic Xbow Quiv', 'mxbq', 'xboq', NULL, 0.0, 1.0, 'rarm', 'larm', NULL, 'xbow', 0.0, 1.0, 0.0, 0.0, 1.0, 1.0, 0.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'misc', 0.0);
INSERT INTO "ItemTypes" VALUES ('Chipped Gem', 'gem0', 'gem', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, NULL, 1.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'misc', 0.0);
INSERT INTO "ItemTypes" VALUES ('Flawed Gem', 'gem1', 'gem', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, NULL, 1.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'misc', 0.0);
INSERT INTO "ItemTypes" VALUES ('Standard Gem', 'gem2', 'gem', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, NULL, 1.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'misc', 0.0);
INSERT INTO "ItemTypes" VALUES ('Flawless Gem', 'gem3', 'gem', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, NULL, 1.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'misc', 0.0);
INSERT INTO "ItemTypes" VALUES ('Perfect Gem', 'gem4', 'gem', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, NULL, 1.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'misc', 0.0);
INSERT INTO "ItemTypes" VALUES ('Amethyst', 'gema', 'gem', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, NULL, 1.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'misc', 0.0);
INSERT INTO "ItemTypes" VALUES ('Diamond', 'gemd', 'gem', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, NULL, 1.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'misc', 0.0);
INSERT INTO "ItemTypes" VALUES ('Emerald', 'geme', 'gem', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, NULL, 1.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'misc', 0.0);
INSERT INTO "ItemTypes" VALUES ('Ruby', 'gemr', 'gem', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, NULL, 1.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'misc', 0.0);
INSERT INTO "ItemTypes" VALUES ('Sapphire', 'gems', 'gem', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, NULL, 1.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'misc', 0.0);
INSERT INTO "ItemTypes" VALUES ('Topaz', 'gemt', 'gem', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, NULL, 1.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'misc', 0.0);
INSERT INTO "ItemTypes" VALUES ('Skull', 'gemz', 'gem', NULL, 0.0, 0.0, NULL, NULL, NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, NULL, 1.0, 0.0, 0.0, 25.0, 0.0, 40.0, 0.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'misc', 0.0);
INSERT INTO "ItemTypes" VALUES ('Shield', 'shie', 'shld', NULL, 1.0, 1.0, 'rarm', 'larm', NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 3.0, 25.0, 3.0, 40.0, 4.0, 0.0, 3.0, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'armo', 0.0);
INSERT INTO "ItemTypes" VALUES ('Scepter', 'scep', 'rod', NULL, 1.0, 1.0, 'rarm', 'larm', NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 3.0, 25.0, 5.0, 40.0, 6.0, 0.0, 1.0, 'pal', NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'weap', 0.0);
INSERT INTO "ItemTypes" VALUES ('Wand', 'wand', 'rod', NULL, 1.0, 1.0, 'rarm', 'larm', NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 2.0, 25.0, 2.0, 40.0, 2.0, 0.0, 1.0, 'nec', NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'weap', 0.0);
INSERT INTO "ItemTypes" VALUES ('Staff', 'staf', 'rod', NULL, 1.0, 1.0, 'rarm', 'larm', NULL, NULL, 0.0, 0.0, 0.0, 0.0, NULL, 1.0, 0.0, 0.0, 5.0, 25.0, 6.0, 40.0, 6.0, 0.0, 1.0, 'sor', NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, 'weap', 0.0);

PRAGMA foreign_keys = true;
