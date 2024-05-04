DROP PROCEDURE IF EXISTS add_boss;

CREATE PROCEDURE add_boss(bossName VARCHAR(255))
LANGUAGE SQL
AS $$
    INSERT INTO counter (name) values (bossName)
$$;