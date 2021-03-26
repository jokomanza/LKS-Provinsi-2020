package com.latihanlks.pc_kab_klaten_joko_supriyanto;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.widget.Button;
import android.widget.Toast;

public class BeliProduct extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_beli_product);
        
        ((Button)findViewById(R.id.btnConfirmBeli)).setOnClickListener(v -> {
            Toast.makeText(this, "Beli", Toast.LENGTH_SHORT).show();
        });
    }
}