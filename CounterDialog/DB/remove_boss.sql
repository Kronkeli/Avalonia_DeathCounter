DROP PROCEDURE IF EXISTS remove_boss;

CREATE PROCEDURE remove_boss(bossName VARCHAR(255))
LANGUAGE SQL
AS $$
    DELETE FROM counter WHERE name = bossName;
    
    -- Resetoidaan counter SEQUENCE manuaalisesti :))))
    ALTER SEQUENCE counter_id_seq restart;
$$;