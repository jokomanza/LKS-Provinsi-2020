package com.latihanlks.pc_kab_klaten_joko_supriyanto.ui.notifications;

import android.content.Intent;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.constraintlayout.widget.ConstraintLayout;
import androidx.fragment.app.Fragment;
import androidx.lifecycle.Observer;
import androidx.lifecycle.ViewModelProvider;

import com.latihanlks.pc_kab_klaten_joko_supriyanto.AsetDetailActivity;
import com.latihanlks.pc_kab_klaten_joko_supriyanto.R;

public class NotificationsFragment extends Fragment {

    private NotificationsViewModel notificationsViewModel;

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {
        notificationsViewModel =
                new ViewModelProvider(this).get(NotificationsViewModel.class);
        View root = inflater.inflate(R.layout.fragment_notifications, container, false);

        ((ConstraintLayout)root.findViewById(R.id.parentAsset)).setOnClickListener(v -> {
            Intent intent = new Intent(getContext(), AsetDetailActivity.class);
            startActivity(intent);
        });
        return root;
    }
}