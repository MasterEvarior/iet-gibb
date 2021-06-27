package dev.giannin.securitymodulebackend.model.user;

import org.springframework.security.core.GrantedAuthority;

import java.util.ArrayList;
import java.util.Collection;

public class UserDetails implements org.springframework.security.core.userdetails.UserDetails {

    private final String password;
    private final String username;

    public UserDetails(String password, String username){
        this.password = password;
        this.username = username;
    }

    @Override
    public Collection<? extends GrantedAuthority> getAuthorities() {
        //Can be left empty because we dont use role based access
        return new ArrayList<>();
    }

    @Override
    public String getPassword() {
        return this.password;
    }

    @Override
    public String getUsername() {
        return this.username;
    }

    @Override
    public boolean isAccountNonExpired() {
        //Always set to true
        return true;
    }

    @Override
    public boolean isAccountNonLocked() {
        //Always set to true
        return true;
    }

    @Override
    public boolean isCredentialsNonExpired() {
        //Always set to true
        return true;
    }

    @Override
    public boolean isEnabled() {
        //Always set to true
        return true;
    }
}
