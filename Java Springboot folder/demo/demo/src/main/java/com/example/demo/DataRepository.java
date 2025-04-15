package com.example.demo;

import org.springframework.jdbc.core.BeanPropertyRowMapper;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.stereotype.Repository;

import java.sql.ResultSet;
import java.util.List;

@Repository
public class DataRepository {

    private final JdbcTemplate jdbcTemplate;

    public DataRepository(JdbcTemplate jdbcTemplate) {
        this.jdbcTemplate = jdbcTemplate;
    }
    public void insertData(int score,String game,String name) {
        jdbcTemplate.update(
                "INSERT INTO scores (name, game, score) VALUES (?, ?, ?) " +
                        "ON DUPLICATE KEY UPDATE score = GREATEST(score, VALUES(score))",
                name, game, score
        );
    }
    public int getMaxScorePacman() {
        Integer highestScore = jdbcTemplate.query(
                "SELECT score FROM scores WHERE game = 'pacman' ORDER BY score DESC LIMIT 1",
                rs -> rs.next() ? rs.getInt("score") : 0
        );
        return highestScore;
    }

    public int getMaxScoreDino() {
        Integer highestScore = jdbcTemplate.query(
                "SELECT score FROM scores WHERE game = 'dino' ORDER BY score DESC LIMIT 1",
                rs -> rs.next() ? rs.getInt("score") : 0
        );
        return highestScore;
    }

    public void addUser(String name) {
        jdbcTemplate.update(
                "INSERT IGNORE INTO users (name) VALUES (?)",
                name
        );
    }
}
