USE football_system;

-- Сезон
INSERT INTO Season (year_start, year_end)
VALUES (2025, 2026);

-- Лига
INSERT INTO League (name, season_id)
VALUES ('National League', 1);

-- Отбори
INSERT INTO Team (name, city, founded_year, league_id) VALUES
('Black Eagles', 'Sofia', 1920, 1),
('Red Warriors', 'Plovdiv', 1935, 1),
('Blue Sharks', 'Varna', 1940, 1),
('Golden Lions', 'Burgas', 1915, 1);

-- Играчите (10)
INSERT INTO Player (first_name, last_name, birth_date, position, team_id) VALUES
('Ivan', 'Petrov', '2000-05-10', 'Forward', 1),
('Georgi', 'Dimitrov', '1998-03-12', 'Midfielder', 1),
('Martin', 'Ivanov', '2001-07-21', 'Defender', 2),
('Petar', 'Georgiev', '1999-01-15', 'Goalkeeper', 2),
('Nikolay', 'Kolev', '2002-11-05', 'Forward', 3),
('Stefan', 'Todorov', '2000-02-18', 'Defender', 3),
('Dimitar', 'Hristov', '1997-09-09', 'Midfielder', 4),
('Viktor', 'Angelov', '2003-04-25', 'Forward', 4),
('Rosen', 'Iliev', '1996-12-30', 'Defender', 1),
('Daniel', 'Nikolov', '2002-08-08', 'Midfielder', 2);

-- Мачове
INSERT INTO MatchGame (match_date, season_id, home_team_id, away_team_id, home_score, away_score)
VALUES
('2026-03-01', 1, 1, 2, 2, 1),
('2026-03-05', 1, 3, 4, 0, 0);
