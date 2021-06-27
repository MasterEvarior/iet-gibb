package dev.giannin.securitymodulebackend.service;

import dev.giannin.securitymodulebackend.model.user.CreateUserDto;
import dev.giannin.securitymodulebackend.model.user.UserDetails;
import dev.giannin.securitymodulebackend.model.user.UserEntity;
import dev.giannin.securitymodulebackend.repository.UserRepository;
import org.apache.commons.collections4.IteratorUtils;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.stereotype.Service;
import org.springframework.web.util.HtmlUtils;

import java.util.InputMismatchException;
import java.util.List;
import java.util.Optional;

@Service
public class UserService implements org.springframework.security.core.userdetails.UserDetailsService{
    private UserRepository repository;
    private final BCryptPasswordEncoder passwordEncoder;
    private static final int LOG_ROUNDS = 13;

    @Autowired
    public UserService(UserRepository repository) {
        this.repository = repository;
        this.passwordEncoder = new BCryptPasswordEncoder(LOG_ROUNDS);
    }

    public UserEntity createUser(CreateUserDto dto) {
        String hashedPassword = this.passwordEncoder.encode(dto.getPassword());
        String escapedUsername = HtmlUtils.htmlEscape(dto.getUsername());

        Optional<UserEntity> optional = repository.findByUsername(escapedUsername);
        if(optional.isPresent()){
            throw new InputMismatchException("Username is already taken");
        }

        UserEntity newUser = UserEntity.builder()
                .username(escapedUsername)
                .password(hashedPassword)
                .build();

        return repository.save(newUser);
    }

    public List<UserEntity> getAllUsers(){
        return IteratorUtils.toList(this.repository.findAll().iterator());
    }

    public Optional<UserEntity> getUserById(Long id){
        return this.repository.findById(id);
    }

    @Override
    public org.springframework.security.core.userdetails.UserDetails loadUserByUsername(String username) throws UsernameNotFoundException {
        Optional<UserEntity> result = this.repository.findByUsername(username);

        if(result.isEmpty()){
            throw new UsernameNotFoundException(username);
        }

        UserEntity foundUser = result.get();

        return new UserDetails(foundUser.getPassword(), foundUser.getUsername());
    }
}
