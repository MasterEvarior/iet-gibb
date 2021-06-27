package dev.giannin.securitymodulebackend.model.error;

import lombok.Getter;

import java.sql.Timestamp;

@Getter
public class ErrorDto {
    private final Timestamp timestamp;
    private final int responseStatus;
    private final String responseReason;
    private final String message;

    public ErrorDto(int responseStatus, String responseReason, String message){
        this.responseStatus = responseStatus;
        this.responseReason = responseReason;
        this.message = message;
        this.timestamp = new Timestamp(System.currentTimeMillis());
    }
}
