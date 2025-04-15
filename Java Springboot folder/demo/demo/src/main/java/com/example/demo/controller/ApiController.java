package com.example.demo.controller;

import com.example.demo.DataRepository;
import com.example.demo.model.DataModel;
import com.example.demo.model.User;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/api")
public class ApiController {

    @GetMapping("/test")
    public String testConnection() {
        return "Hello from Java API!";
    }

    @CrossOrigin(origins = "*")
    @PostMapping("/send")
    public String receiveData(@RequestBody DataModel data) {
        System.out.println("Received from C#: " + data.getScore());
        return "Received: " + data.getScore();
    }
    @Autowired
    private DataRepository dataRepository;

    @PostMapping("/add")
    public String addData(@RequestBody DataModel data) {
        dataRepository.insertData(data.getScore(),data.getGame(),data.getName());
        return "Data added successfully!";
    }

    @GetMapping("/scorePacman")
    public int sendScore(){
        return dataRepository.getMaxScorePacman();
    }

    @GetMapping("/scoreDino")
    public int sendScoreDino(){
        return dataRepository.getMaxScoreDino();
    }
    @PostMapping("/addUser")
    public String addUser(@RequestBody User user) {
        dataRepository.addUser(user.getName());
        return "User added successfully!";
    }
}
