CREATE DATABASE IF NOT EXISTS football_system;
USE football_system;

CREATE TABLE Season (
    season_id INT PRIMARY KEY AUTO_INCREMENT,
    year_start INT NOT NULL,
    year_end INT NOT NULL,
    UNIQUE(year_start, year_end),
    CHECK (year_end = year_start + 1)
);

CREATE TABLE League (
    league_id INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(100) NOT NULL,
    season_id INT NOT NULL,
    FOREIGN KEY (season_id) REFERENCES Season(season_id),
    UNIQUE(name, season_id)
);

CREATE TABLE Team (
    team_id INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(100) NOT NULL UNIQUE,
    city VARCHAR(100) NOT NULL,
    founded_year INT CHECK (founded_year > 1850),
    league_id INT NOT NULL,
    FOREIGN KEY (league_id) REFERENCES League(league_id)
);

CREATE TABLE Player (
    player_id INT PRIMARY KEY AUTO_INCREMENT,
    first_name VARCHAR(100) NOT NULL,
    last_name VARCHAR(100) NOT NULL,
    birth_date DATE NOT NULL,
    position VARCHAR(50) NOT NULL,
    team_id INT NOT NULL,
    FOREIGN KEY (team_id) REFERENCES Team(team_id)
);

CREATE TABLE MatchGame (
    match_id INT PRIMARY KEY AUTO_INCREMENT,
    match_date DATE NOT NULL,
    season_id INT NOT NULL,
    home_team_id INT NOT NULL,
    away_team_id INT NOT NULL,
    home_score INT DEFAULT 0 CHECK (home_score >= 0),
    away_score INT DEFAULT 0 CHECK (away_score >= 0),
    FOREIGN KEY (season_id) REFERENCES Season(season_id),
    FOREIGN KEY (home_team_id) REFERENCES Team(team_id),
    FOREIGN KEY (away_team_id) REFERENCES Team(team_id),
    CHECK (home_team_id <> away_team_id)
);
