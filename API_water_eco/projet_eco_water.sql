-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Hôte : 127.0.0.1
-- Généré le : ven. 05 avr. 2024 à 08:38
-- Version du serveur : 10.4.32-MariaDB
-- Version de PHP : 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données : `projet_eco_water`
--

-- --------------------------------------------------------

--
-- Structure de la table `kc`
--

CREATE TABLE `kc` (
  `kc_id` int(11) NOT NULL,
  `kc_name` varchar(50) NOT NULL,
  `kc_need` decimal(15,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Déchargement des données de la table `kc`
--

INSERT INTO `kc` (`kc_id`, `kc_name`, `kc_need`) VALUES
(1, 'Tomate', 0.65),
(2, 'Coquelicot', 0.50),
(3, 'Blé', 0.80),
(4, 'Carotte', 0.40),
(5, 'Maïs', 0.60),
(6, 'Betterave', 0.20),
(7, 'Courgette', 0.45),
(8, 'Pomme de terre', 0.70),
(9, 'Aubergine', 0.48),
(10, 'Soja', 0.10),
(11, 'Riz', 0.30),
(12, 'Oignon', 0.50),
(13, 'Fraise', 0.30);

-- --------------------------------------------------------

--
-- Structure de la table `surfaceculture`
--

CREATE TABLE `surfaceculture` (
  `sur_id` int(11) NOT NULL,
  `sur_value` decimal(15,2) NOT NULL,
  `sur_unit` varchar(50) NOT NULL,
  `wat_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Déchargement des données de la table `surfaceculture`
--

INSERT INTO `surfaceculture` (`sur_id`, `sur_value`, `sur_unit`, `wat_id`) VALUES
(1, 100.00, 'm^2', 1),
(2, 250.00, 'm^2', 5),
(3, 50.00, 'm^2', 7),
(4, 300.00, 'm^2', 8);

-- --------------------------------------------------------

--
-- Structure de la table `watervolume`
--

CREATE TABLE `watervolume` (
  `wat_id` int(11) NOT NULL,
  `wat_max_volume` decimal(15,2) NOT NULL,
  `wat_current_volume` decimal(15,2) NOT NULL,
  `wat_unit` varchar(50) NOT NULL,
  `wat_insee` int(11) NOT NULL,
  `wat_name` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Déchargement des données de la table `watervolume`
--

INSERT INTO `watervolume` (`wat_id`, `wat_max_volume`, `wat_current_volume`, `wat_unit`, `wat_insee`, `wat_name`) VALUES
(1, 500.00, 500.00, 'Litre(s)', 30189, 'Réserve Nîmoise'),
(2, 380.00, 380.00, 'Litre(s)', 34172, 'Réserve Montpellieraine'),
(3, 550.00, 550.00, 'Litre(s)', 31555, 'Réserve Toulousaine'),
(5, 200.00, 200.00, 'Litre(s)', 13055, 'Réserve Marseillaise'),
(6, 400.00, 400.00, 'Litre(s)', 11262, 'Réserve Narbonnaise'),
(7, 650.00, 650.00, 'Litre(s)', 13022, 'Réserve de Cassis'),
(8, 800.00, 800.00, 'Litre(s)', 59350, 'Réserve Lilloise'),
(9, 700.00, 700.00, 'Litre(s)', 35238, 'Réserve de Rennes');

--
-- Index pour les tables déchargées
--

--
-- Index pour la table `kc`
--
ALTER TABLE `kc`
  ADD PRIMARY KEY (`kc_id`);

--
-- Index pour la table `surfaceculture`
--
ALTER TABLE `surfaceculture`
  ADD PRIMARY KEY (`sur_id`),
  ADD KEY `wat_id` (`wat_id`);

--
-- Index pour la table `watervolume`
--
ALTER TABLE `watervolume`
  ADD PRIMARY KEY (`wat_id`);

--
-- AUTO_INCREMENT pour les tables déchargées
--

--
-- AUTO_INCREMENT pour la table `kc`
--
ALTER TABLE `kc`
  MODIFY `kc_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- AUTO_INCREMENT pour la table `surfaceculture`
--
ALTER TABLE `surfaceculture`
  MODIFY `sur_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT pour la table `watervolume`
--
ALTER TABLE `watervolume`
  MODIFY `wat_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- Contraintes pour les tables déchargées
--

--
-- Contraintes pour la table `surfaceculture`
--
ALTER TABLE `surfaceculture`
  ADD CONSTRAINT `surfaceculture_ibfk_1` FOREIGN KEY (`wat_id`) REFERENCES `watervolume` (`wat_id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
