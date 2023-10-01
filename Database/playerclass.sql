/*
 Navicat Premium Data Transfer

 Source Server         : D2SLibData
 Source Server Type    : SQLite
 Source Server Version : 3035005 (3.35.5)
 Source Schema         : main

 Target Server Type    : SQLite
 Target Server Version : 3035005 (3.35.5)
 File Encoding         : 65001

 Date: 01/10/2023 01:04:37
*/

PRAGMA foreign_keys = false;

-- ----------------------------
-- Table structure for playerclass
-- ----------------------------
DROP TABLE IF EXISTS "playerclass";
CREATE TABLE "playerclass" (
  "Id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
  "Class" TEXT NOT NULL,
  "Code" text(3) NOT NULL
);

-- ----------------------------
-- Records of playerclass
-- ----------------------------
INSERT INTO "playerclass" VALUES (1, 'Amazon', 'ama');
INSERT INTO "playerclass" VALUES (2, 'Sorceress', 'sor');
INSERT INTO "playerclass" VALUES (3, 'Necromancer', 'nec');
INSERT INTO "playerclass" VALUES (4, 'Paladin', 'pal');
INSERT INTO "playerclass" VALUES (5, 'Barbarian', 'bar');
INSERT INTO "playerclass" VALUES (6, 'Druid', 'dru');
INSERT INTO "playerclass" VALUES (7, 'Assassin', 'ass');

-- ----------------------------
-- Auto increment value for playerclass
-- ----------------------------
UPDATE "sqlite_sequence" SET seq = 8 WHERE name = 'playerclass';

PRAGMA foreign_keys = true;
