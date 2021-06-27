package dev.giannin.securitymodulebackend.controller;

import dev.giannin.securitymodulebackend.model.user.CreateUserDto;
import dev.giannin.securitymodulebackend.model.user.UserEntity;
import dev.giannin.securitymodulebackend.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import javax.validation.Valid;
import java.util.List;
import java.util.Optional;

@RestController
@RequestMapping("/users")
public class UserController {
    private UserService service;

    @Autowired
    public UserController(UserService service){
        this.service = service;
    }

    @ResponseBody
    @PostMapping(consumes = "application/json", produces = "application/json")
    public ResponseEntity<UserEntity> createUser(@Valid @RequestBody CreateUserDto dto){
        UserEntity createdUser = this.service.createUser(dto);
        return new ResponseEntity<>(createdUser, HttpStatus.CREATED);
    }

    @ResponseBody
    @GetMapping(produces = "application/json")
    public ResponseEntity<List<UserEntity>> getAllUsers(){
        return ResponseEntity.ok(this.service.getAllUsers());
    }

    @GetMapping(path = "/{id}", produces = "application/json")
    public ResponseEntity<Object> getUserById(@PathVariable Long id){
        Optional<UserEntity> returnValue = this.service.getUserById(id);
        if(returnValue.isPresent()){
            return ResponseEntity.ok(returnValue.get());
        }else{
            return ResponseEntity.notFound().build();
        }
    }

}
