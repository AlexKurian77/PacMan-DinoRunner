package com.example.demo.controller;

import com.example.demo.model.User;
import com.example.demo.repository.UserRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/users")
public class UserController {

    @Autowired
    private UserRepository userRepository;

//    @PostMapping("/add")
//    public String addUser(@RequestBody User user) {
//        userRepository.saveUser(user);
//        return "User added successfully!";
//    }
}
