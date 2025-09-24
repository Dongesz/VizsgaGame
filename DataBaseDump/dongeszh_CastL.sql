-- phpMyAdmin SQL Dump
-- version 5.2.2
-- https://www.phpmyadmin.net/
--
-- Gép: localhost:3306
-- Létrehozás ideje: 2025. Sze 24. 17:51
-- Kiszolgáló verziója: 10.11.14-MariaDB-cll-lve
-- PHP verzió: 8.4.11

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `dongeszh_CastL`
--

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `scoreboard`
--

CREATE TABLE `scoreboard` (
  `user_id` bigint(20) UNSIGNED NOT NULL,
  `total_score` bigint(20) UNSIGNED DEFAULT 0,
  `wins` int(10) UNSIGNED DEFAULT 0,
  `rounds` int(10) UNSIGNED DEFAULT 0,
  `last_updated` timestamp NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- A tábla adatainak kiíratása `scoreboard`
--

INSERT INTO `scoreboard` (`user_id`, `total_score`, `wins`, `rounds`, `last_updated`) VALUES
(1, 4200, 32, 80, '2025-09-18 07:23:59'),
(2, 1500, 12, 40, '2025-09-18 07:23:59'),
(3, 3100, 25, 70, '2025-09-18 07:23:59'),
(4, 900, 6, 25, '2025-09-18 07:23:59'),
(5, 2800, 20, 65, '2025-09-18 07:23:59'),
(6, 2200, 18, 55, '2025-09-18 07:23:59'),
(7, 1300, 10, 30, '2025-09-18 07:23:59'),
(8, 3600, 28, 75, '2025-09-18 07:23:59'),
(9, 2000, 15, 50, '2025-09-18 07:23:59'),
(10, 2700, 22, 60, '2025-09-18 07:23:59'),
(11, 1600, 11, 35, '2025-09-18 07:23:59'),
(12, 800, 5, 20, '2025-09-18 07:23:59'),
(13, 3400, 26, 72, '2025-09-18 07:23:59'),
(14, 1100, 7, 28, '2025-09-18 07:23:59'),
(16, 1900, 13, 45, '2025-09-18 07:23:59'),
(17, 2300, 17, 52, '2025-09-18 07:23:59'),
(18, 1250, 8, 27, '2025-09-18 07:23:59'),
(19, 2600, 19, 58, '2025-09-18 07:23:59'),
(20, 1750, 12, 42, '2025-09-18 07:23:59'),
(21, 3200, 24, 68, '2025-09-18 07:23:59'),
(22, 950, 6, 23, '2025-09-18 07:23:59'),
(23, 1400, 9, 31, '2025-09-18 07:23:59'),
(24, 2950, 21, 63, '2025-09-18 07:23:59'),
(26, 1700, 11, 37, '2025-09-18 07:23:59'),
(27, 2100, 15, 49, '2025-09-18 07:23:59'),
(28, 600, 3, 15, '2025-09-18 07:23:59'),
(29, 2500, 18, 55, '2025-09-18 07:23:59'),
(30, 4000, 31, 85, '2025-09-18 07:23:59');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `users`
--

CREATE TABLE `users` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `name` varchar(100) NOT NULL,
  `email` varchar(255) NOT NULL,
  `password_hash` varbinary(255) NOT NULL,
  `user_type` enum('user','admin') NOT NULL DEFAULT 'user',
  `created_at` timestamp NULL DEFAULT current_timestamp(),
  `updated_at` timestamp NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- A tábla adatainak kiíratása `users`
--

INSERT INTO `users` (`id`, `name`, `email`, `password_hash`, `user_type`, `created_at`, `updated_at`) VALUES
(1, 'dongesz', 'dongesz@mail.com', 0x68617368, 'admin', '2025-09-18 07:23:46', '2025-09-18 07:23:46'),
(2, 'shadowfang', 'shadowfang@gmail.com', 0x68617368, 'user', '2025-09-18 07:23:46', '2025-09-18 07:23:46'),
(3, 'pixeldragon', 'pixeldragon@outlook.com', 0x68617368, 'user', '2025-09-18 07:23:46', '2025-09-18 07:23:46'),
(4, 'ironclaw', 'ironclaw@yahoo.com', 0x68617368, 'user', '2025-09-18 07:23:46', '2025-09-18 07:23:46'),
(5, 'crimsonwolf', 'crimsonwolf@mail.com', 0x68617368, 'user', '2025-09-18 07:23:46', '2025-09-18 07:23:46'),
(6, 'neonrider', 'neonrider@gmail.com', 0x68617368, 'user', '2025-09-18 07:23:46', '2025-09-18 07:23:46'),
(7, 'blazebird', 'blazebird@hotmail.com', 0x68617368, 'user', '2025-09-18 07:23:46', '2025-09-18 07:23:46'),
(8, 'admin', '', 0x68617368, 'admin', '2025-09-18 07:23:46', '2025-09-18 07:51:30'),
(9, 'vortexian', 'vortexian@gmail.com', 0x68617368, 'user', '2025-09-18 07:23:46', '2025-09-18 07:23:46'),
(10, 'stormveil', 'stormveil@outlook.com', 0x68617368, 'user', '2025-09-18 07:23:46', '2025-09-18 07:23:46'),
(11, 'lunaris', 'lunaris@mail.com', 0x68617368, 'user', '2025-09-18 07:23:46', '2025-09-18 07:23:46'),
(12, 'cyberrook', 'cyberrook@gmail.com', 0x68617368, 'user', '2025-09-18 07:23:46', '2025-09-18 07:23:46'),
(13, 'emberlord', 'emberlord@proton.me', 0x68617368, 'user', '2025-09-18 07:23:46', '2025-09-18 07:23:46'),
(14, 'glitcher', 'glitcher@mail.com', 0x68617368, 'user', '2025-09-18 07:23:46', '2025-09-18 07:23:46'),
(16, 'darkember', 'darkember@mail.com', 0x68617368, 'user', '2025-09-18 07:23:46', '2025-09-18 07:23:46'),
(17, 'ironshade', 'ironshade@gmail.com', 0x68617368, 'user', '2025-09-18 07:23:46', '2025-09-18 07:23:46'),
(18, 'skyforge', 'skyforge@mail.com', 0x68617368, 'user', '2025-09-18 07:23:46', '2025-09-18 07:23:46'),
(19, 'crypticowl', 'crypticowl@outlook.com', 0x68617368, 'user', '2025-09-18 07:23:46', '2025-09-18 07:23:46'),
(20, 'stormreaper', 'stormreaper@mail.com', 0x68617368, 'user', '2025-09-18 07:23:46', '2025-09-18 07:23:46'),
(21, 'ashenblade', 'ashenblade@gmail.com', 0x68617368, 'user', '2025-09-18 07:23:46', '2025-09-18 07:23:46'),
(22, 'nightcrawler', 'nightcrawler@mail.com', 0x68617368, 'user', '2025-09-18 07:23:46', '2025-09-18 07:23:46'),
(23, 'arcadian', 'arcadian@mail.com', 0x68617368, 'user', '2025-09-18 07:23:46', '2025-09-18 07:23:46'),
(24, 'hollowbane', 'hollowbane@gmail.com', 0x68617368, 'user', '2025-09-18 07:23:46', '2025-09-18 07:23:46'),
(26, 'phantomflux', 'phantomflux@gmail.com', 0x68617368, 'user', '2025-09-18 07:23:46', '2025-09-18 07:23:46'),
(27, 'novafox', 'novafox@mail.com', 0x68617368, 'user', '2025-09-18 07:23:46', '2025-09-18 07:23:46'),
(28, 'bytehunter', 'bytehunter@mail.com', 0x68617368, 'user', '2025-09-18 07:23:46', '2025-09-18 07:23:46'),
(29, 'zephyrix', 'zephyrix@mail.com', 0x68617368, 'user', '2025-09-18 07:23:46', '2025-09-18 07:23:46'),
(30, 'gravemind', 'gravemind@gmail.com', 0x68617368, 'user', '2025-09-18 07:23:46', '2025-09-18 07:23:46');

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `scoreboard`
--
ALTER TABLE `scoreboard`
  ADD PRIMARY KEY (`user_id`);

--
-- A tábla indexei `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `email` (`email`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `users`
--
ALTER TABLE `users`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=31;

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `scoreboard`
--
ALTER TABLE `scoreboard`
  ADD CONSTRAINT `fk_scoreboard_user` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
