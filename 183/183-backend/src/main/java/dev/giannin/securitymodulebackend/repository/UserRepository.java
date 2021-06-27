package dev.giannin.securitymodulebackend.repository;

import dev.giannin.securitymodulebackend.model.user.UserEntity;
import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;

import java.util.Optional;

@Repository
public interface UserRepository extends CrudRepository<UserEntity, Long> {
    public Optional<UserEntity> findByUsername(String username);
}
