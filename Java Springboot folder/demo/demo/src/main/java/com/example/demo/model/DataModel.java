package com.example.demo.model;

public class DataModel {
    private int score;
    private String game;
    private String name;
    public int getScore() {
        return score;
    }
    public String getGame() {return game;}
    public String getName() {return name;}

    public void setName(String name) {this.name = name;}
    public void setGame(String game) {this.game = game;}
    public void setScore(int sc) {
        this.score = sc;
    }
}
